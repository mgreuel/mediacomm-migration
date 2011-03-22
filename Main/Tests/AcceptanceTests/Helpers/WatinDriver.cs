namespace MediaCommMVC.Tests.AcceptanceTests.Helpers
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using MvcContrib.TestHelper.Ui;

    using WatiN.Core;
    using WatiN.Core.Native.Windows;

    #endregion

    /// <summary>
    /// from http://mvccontrib.codeplex.com/
    /// </summary>
    public class WatinDriver
    {
        #region Constants and Fields

        private readonly string baseurl;

        private Browser browser;

        #endregion

        #region Constructors and Destructors

        public WatinDriver(Browser browser, string baseurl)
        {
            this.baseurl = baseurl;
            this.browser = browser;
            this.browser.ShowWindow(NativeMethods.WindowShowStyle.Maximize);
        }

        #endregion

        #region Properties

        public virtual string Url
        {
            get
            {
                return this.browser.Url.Replace(this.baseurl, string.Empty);
            }
        }

        #endregion

        #region Public Methods

        public virtual void ClickButton(string value)
        {
            this.browser.Button(Find.ById(value)).Click();
        }

        public virtual void ClickLink(string value)
        {
            this.browser.Link(Find.By("rel", value)).Click();
        }

        public virtual void ClickRowLink<T>(string tableName, List<RowFilter<T>> filters, string relId)
        {
            TableRow filteredRow = this.GetFilteredRows(tableName, filters)[0];
            var link = filteredRow.Link(Find.By("rel", relId));
            link.Click();
        }

        public virtual void Dispose()
        {
            this.browser.Close();
            this.browser.Dispose();
            this.browser = null;
        }

        public virtual void ExecuteScript(string script)
        {
            this.browser.RunScript(script);
        }

        public virtual string GetHtml(string id)
        {
            Element textField = this.browser.Element(Find.ById(id));
            if (textField == null)
            {
                throw new Exception(string.Format("Could not find element '{0}' on form.", id));
            }

            return textField.InnerHtml;
        }

        public virtual int GetRowCount<T>(string tableName, List<RowFilter<T>> filters)
        {
            List<TableRow> filteredRows = this.GetFilteredRows(tableName, filters);
            return filteredRows.Count;
        }

        public virtual string GetTestname()
        {
            var stack = new StackTrace();
            StackFrame testMethodFrame =
                stack.GetFrames().Where(frame => frame.GetMethod().ReflectedType.Assembly != this.GetType().Assembly).
                    FirstOrDefault();
            return testMethodFrame.GetMethod().Name;
        }

        public virtual string GetValue(string id)
        {
            TextField textField = this.browser.TextField(Find.ById(id));
            if (textField == null)
            {
                throw new Exception(string.Format("Could not find field '{0}' on form.", id));
            }

            return textField.Value;
        }

        public virtual WatinDriver Navigate(string url)
        {
            this.browser.GoTo(this.baseurl + url);
            return this;
        }

        public virtual void SetValue(string name, string value)
        {
            TextField textField = this.browser.TextField(Find.ByName(name));
            if (textField.Exists)
            {
                textField.Value = value;
                return;
            }

            SelectList select = this.browser.SelectList(Find.ByName(name));
            if (select.Exists)
            {
                select.Select(value);
                return;
            }

            throw new InvalidOperationException("Could not find a HTML Element by the name " + name);
        }

        #endregion

        #region Methods

        private List<TableRow> GetFilteredRows<T>(string tableName, List<RowFilter<T>> filters)
        {
            Table table = this.browser.Table(tableName);
            var rows = table.TableRows;

            var filteredRows = new List<TableRow>();
            foreach (var filter in filters)
            {
                filteredRows.AddRange(
                    rows.Where(
                        row => row.TableCells.Any(cell => cell.Text != null ? cell.Text.Contains(filter.Value) : false)));
            }

            return filteredRows;
        }

        #endregion
    }
}