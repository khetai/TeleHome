
(function ($) {
	"use strict";
		$.fn.meanmenu = function (options) {
				var defaults = {
						meanmenutarget: jquery(this), // target the current html markup you wish to replace
						meanmenucontainer: 'body', // choose where meanmenu will be placed within the html
						meanmenuclose: "x", // single character you want to represent the close menu button
						meanmenuclosesize: "18px", // set font size of close button
						meanmenuopen: "<span /><span /><span />", // text/markup you want when menu is closed
						meanrevealposition: "right", // left right or center positions
						meanrevealpositiondistance: "0", // tweak the position of the menu
						meanrevealcolour: "", // override css colours for the reveal background
						meanscreenwidth: "480", // set the screen width you want meanmenu to kick in at
						meannavpush: "", // set a height here in px, em or % if you want to budge your layout now the navigation is missing.
						meanshowchildren: true, // true to show children in the menu, false to hide them
						meanexpandablechildren: true, // true to allow expand/collapse children
						meanremoveattrs: false, // true to remove classes and ids, false to keep them
						onepage: false, // set to true for one page sites
						meandisplay: "block", // override display method for table cell based layouts e.g. table-cell
						removeelements: "" // set to hide page elements
				};
				options = $.extend(defaults, options);

				// get browser width
				var currentwidth = window.innerwidth || document.documentelement.clientwidth;

				return this.each(function () {
						var meanmenu = options.meanmenutarget;
						var meancontainer = options.meanmenucontainer;
						var meanmenuclose = options.meanmenuclose;
						var meanmenuclosesize = options.meanmenuclosesize;
						var meanmenuopen = options.meanmenuopen;
						var meanrevealposition = options.meanrevealposition;
						var meanrevealpositiondistance = options.meanrevealpositiondistance;
						var meanrevealcolour = options.meanrevealcolour;
						var meanscreenwidth = options.meanscreenwidth;
						var meannavpush = options.meannavpush;
						var meanrevealclass = ".meanmenu-reveal";
						var meanshowchildren = options.meanshowchildren;
						var meanexpandablechildren = options.meanexpandablechildren;
						var meanexpand = options.meanexpand;
						var meanremoveattrs = options.meanremoveattrs;
						var onepage = options.onepage;
						var meandisplay = options.meandisplay;
						var removeelements = options.removeelements;

						//detect known mobile/tablet usage
						var ismobile = false;
						if ( (navigator.useragent.match(/iphone/i)) || (navigator.useragent.match(/ipod/i)) || (navigator.useragent.match(/ipad/i)) || (navigator.useragent.match(/android/i)) || (navigator.useragent.match(/blackberry/i)) || (navigator.useragent.match(/windows phone/i)) ) {
								ismobile = true;
						}

						if ( (navigator.useragent.match(/msie 8/i)) || (navigator.useragent.match(/msie 7/i)) ) {
							// add scrollbar for ie7 & 8 to stop breaking resize function on small content sites
								jquery('html').css("overflow-y" , "scroll");
						}

						var meanrevealpos = "";
						var meancentered = function() {
							if (meanrevealposition === "center") {
								var newwidth = window.innerwidth || document.documentelement.clientwidth;
								var meancenter = ( (newwidth/2)-22 )+"px";
								meanrevealpos = "left:" + meancenter + ";right:auto;";

								if (!ismobile) {
									jquery('.meanmenu-reveal').css("left",meancenter);
								} else {
									jquery('.meanmenu-reveal').animate({
											left: meancenter
									});
								}
							}
						};

						var menuon = false;
						var meanmenuexist = false;


						if (meanrevealposition === "right") {
								meanrevealpos = "right:" + meanrevealpositiondistance + ";left:auto;";
						}
						if (meanrevealposition === "left") {
								meanrevealpos = "left:" + meanrevealpositiondistance + ";right:auto;";
						}
						// run center function
						meancentered();

						// set all styles for mean-reveal
						var $navreveal = "";

						var meaninner = function() {
								// get last class name
								if (jquery($navreveal).is(".meanmenu-reveal.meanclose")) {
										$navreveal.html(meanmenuclose);
								} else {
										$navreveal.html(meanmenuopen);
								}
						};

						// re-instate original nav (and call this on window.width functions)
						var meanoriginal = function() {
							jquery('.mean-bar,.mean-push').remove();
							jquery(meancontainer).removeclass("mean-container");
							jquery(meanmenu).css('display', meandisplay);
							menuon = false;
							meanmenuexist = false;
							jquery(removeelements).removeclass('mean-remove');
						};

						// navigation reveal
						var showmeanmenu = function() {
								var meanstyles = "background:"+meanrevealcolour+";color:"+meanrevealcolour+";"+meanrevealpos;
								if (currentwidth <= meanscreenwidth) {
								jquery(removeelements).addclass('mean-remove');
									meanmenuexist = true;
									// add class to body so we don't need to worry about media queries here, all css is wrapped in '.mean-container'
									jquery(meancontainer).addclass("mean-container");
									jquery('.mean-container').prepend('<div class="mean-bar"><a href="#nav" class="meanmenu-reveal" style="'+meanstyles+'">show navigation</a><nav class="mean-nav"></nav></div>');

									//push meanmenu navigation into .mean-nav
									var meanmenucontents = jquery(meanmenu).html();
									jquery('.mean-nav').html(meanmenucontents);

									// remove all classes from everything inside meanmenu nav
									if(meanremoveattrs) {
										jquery('nav.mean-nav ul, nav.mean-nav ul *').each(function() {
											// first check if this has mean-remove class
											if (jquery(this).is('.mean-remove')) {
												jquery(this).attr('class', 'mean-remove');
											} else {
												jquery(this).removeattr("class");
											}
											jquery(this).removeattr("id");
										});
									}

									// push in a holder div (this can be used if removal of nav is causing layout issues)
									jquery(meanmenu).before('<div class="mean-push" />');
									jquery('.mean-push').css("margin-top",meannavpush);

									// hide current navigation and reveal mean nav link
									jquery(meanmenu).hide();
									jquery(".meanmenu-reveal").show();

									// turn 'x' on or off
									jquery(meanrevealclass).html(meanmenuopen);
									$navreveal = jquery(meanrevealclass);

									//hide mean-nav ul
									jquery('.mean-nav ul').hide();

									// hide sub nav
									if(meanshowchildren) {
											// allow expandable sub nav(s)
											if(meanexpandablechildren){
												jquery('.mean-nav ul ul').each(function() {
														if(jquery(this).children().length){
																jquery(this,'li:first').parent().append('<a class="mean-expand" href="#" style="font-size: '+ meanmenuclosesize +'">'+ meanexpand +'</a>');
														}
												});
												jquery('.mean-expand').on("click",function(e){
														e.preventdefault();
															if (jquery(this).hasclass("mean-clicked")) {
																jquery(this).prev('ul').slideup(300, function(){});
																jquery(this).parent().removeclass('dropdown-opened');
														} else {
																jquery(this).prev('ul').slidedown(300, function(){});
																jquery(this).parent().addclass('dropdown-opened');
														}
														jquery(this).toggleclass("mean-clicked");
												});
											} else {
													jquery('.mean-nav ul ul').show();
											}
									} else {
											jquery('.mean-nav ul ul').hide();
									}

									// add last class to tidy up borders
									jquery('.mean-nav ul li').last().addclass('mean-last');
									$navreveal.removeclass("meanclose");
									jquery($navreveal).click(function(e){
										e.preventdefault();
								if( menuon === false ) {
												$navreveal.css("text-align", "center");
												$navreveal.css("text-indent", "0");
												$navreveal.css("font-size", meanmenuclosesize);
												jquery('.mean-nav ul:first').slidedown();
												menuon = true;
										} else {
											jquery('.mean-nav ul:first').slideup();
											menuon = false;
										}
											$navreveal.toggleclass("meanclose");
											meaninner();
											jquery(removeelements).addclass('mean-remove');
									});

									// for one page websites, reset all variables...
									if ( onepage ) {
										jquery('.mean-nav ul > li > a:first-child').on( "click" , function () {
											jquery('.mean-nav ul:first').slideup();
											menuon = false;
											jquery($navreveal).toggleclass("meanclose").html(meanmenuopen);
										});
									}
							} else {
								meanoriginal();
							}
						};

						if (!ismobile) {
								// reset menu on resize above meanscreenwidth
								jquery(window).resize(function () {
										currentwidth = window.innerwidth || document.documentelement.clientwidth;
										if (currentwidth > meanscreenwidth) {
												meanoriginal();
										} else {
											meanoriginal();
										}
										if (currentwidth <= meanscreenwidth) {
												showmeanmenu();
												meancentered();
										} else {
											meanoriginal();
										}
								});
						}

					jquery(window).resize(function () {
								// get browser width
								currentwidth = window.innerwidth || document.documentelement.clientwidth;

								if (!ismobile) {
										meanoriginal();
										if (currentwidth <= meanscreenwidth) {
												showmeanmenu();
												meancentered();
										}
								} else {
										meancentered();
										if (currentwidth <= meanscreenwidth) {
												if (meanmenuexist === false) {
														showmeanmenu();
												}
										} else {
												meanoriginal();
										}
								}
						});

					// run main menumenu function on load
					showmeanmenu();
				});
		};
})(jquery);
