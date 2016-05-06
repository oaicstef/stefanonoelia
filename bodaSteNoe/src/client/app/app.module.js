//COUNTDOWN VARIABLES
	var c_days = "",							//Countdown "Days" label
	c_hours = "HOURS",							//Countdown "Hours" label
	c_minutes = "MIN.",							//Countdown "Minutes" label
	c_seconds = "SEC.";							//Countdown "Seconds" label
  
(function () {
  'use strict';
  var supportedLanguages = ['es', 'it'];
  
  var app = angular.module('app', [
    'pascalprecht.translate'
  ]);

  app.config(function ($translateProvider) {
    $translateProvider.useStaticFilesLoader({
      prefix: '/app/localization/',
      suffix: '.json'
    });
    
    var firstLanguage = null;
    
    //For Chrome and Mozilla
    if (window.navigator.languages) {
      window.navigator.languages.forEach(function (lang) {
        lang = lang.substring(0,2);
        
        if (supportedLanguages.find(e => e == lang) && !firstLanguage) {
          firstLanguage = lang;
        }
      }, this);
    } else {
      // For IE
      firstLanguage = window.navigator.language.substring(0, 2);
    }
    
    if (!supportedLanguages.find(e => e == firstLanguage)) {
      firstLanguage = "it";
    }
    $translateProvider.preferredLanguage(firstLanguage);
  });

  app.run(["$http","$translate", "$filter", function ($http, $translate, $filter) {
  
  }]);
})();
