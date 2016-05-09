 
(function () {
  'use strict';
    
  var app = angular.module('app', [
    'pascalprecht.translate'
  ]);

  app.config(function ($translateProvider) {
    $translateProvider.useStaticFilesLoader({
      prefix: '/localization/',
      suffix: '.json'
    });
    
    var supportedLanguages = ['es', 'it'];
    var firstLanguage = null;
    
    //For Chrome and Mozilla
    if (window.navigator.languages) {
      window.navigator.languages.forEach(function (lang) {
        lang = lang.substring(0,2);
        
        if (supportedLanguages.indexOf(lang) > 0 && !firstLanguage) {
          firstLanguage = lang;
        }
      }, this);
    } else if (window.navigator.language)
    {
      // For IE
      firstLanguage = window.navigator.language.substring(0, 2);
    }
    else {
      firstLanguage = 'es';
    }
        
    if (supportedLanguages.indexOf(firstLanguage) < 0) {
      firstLanguage = "it";
    }
    $translateProvider.preferredLanguage(firstLanguage);
  });

  app.run(["$http","$translate", "$filter", function ($http, $translate, $filter) {
  
  }]);
})();
