/**
*	Site-specific configuration settings for Highslide JS
*/
hs.graphicsDir = '/Content/highslide/graphics/';
hs.showCredits = false;
hs.outlineType = 'custom';
hs.dimmingOpacity = 0.75;
hs.fadeInOut = true;
hs.align = 'center';
hs.marginBottom = 70;
hs.marginLeft = 100;
hs.allowMultipleInstances = false;
hs.registerOverlay({
	html: '<div class="closebutton" onclick="return hs.close(this)" title="Close"></div>',
	position: 'top right',
	useOnHtml: true,
	fade: 2 // fading the semi-transparent overlay looks bad in IE
});



// Add the slideshow controller
hs.addSlideshow({
	slideshowGroup: 'group1',
	interval: 5000,
	repeat: false,
	useControls: true,
	fixedControls: false,
	overlayOptions: {
		className: 'large-dark',
		opacity: '1',
		position: 'bottom center',
		offsetX: '50',
		offsetY: '-10',
		relativeTo: 'viewport',
		hideOnMouseOut: false
	},
	thumbstrip: {
		mode: 'vertical',
		position: 'middle left',
		relativeTo: 'viewport'
	}

});

// gallery config object
var config1 = {
	slideshowGroup: 'group1',
	numberPosition: 'caption',
	transitions: ['expand', 'crossfade']
};
