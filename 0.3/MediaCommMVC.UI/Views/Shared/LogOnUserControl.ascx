<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Resources" %>
<%
    if (Request.IsAuthenticated)
    {
%>
<%= General.Welcome %> <b>
    <%= Html.ActionLink(Page.User.Identity.Name, "MyProfile", "Users")%></b>! [<%= Html.ActionLink("Log Off", "LogOff", "Account") %>]
<%
    }
    else
    {
%>
[
<%= Html.ActionLink("Log On", "LogOn", "Account") %>
]
<%
    }
%>
