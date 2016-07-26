
(function () {
  'use strict';

  var app = angular.module('app', [
    'pascalprecht.translate',
    'ui.router'
  ]);

  app.config(['$translateProvider', '$stateProvider', '$urlRouterProvider', function ($translateProvider, $stateProvider, $urlRouterProvider) {
    translationConfiguration($translateProvider);

    routeConfiguration($stateProvider, $urlRouterProvider);
  }]);

  app.run(["$http", "$translate", "$filter", function ($http, $translate, $filter) {
    
  }]);

  function routeConfiguration($stateProvider, $urlRouterProvider) {
    $stateProvider
      .state("blog", {
        url: "/blog",
        //controller: "FeedController",
        templateUrl: "app/views/blog.html",
      })
      .state('home', {
      url: "^/",
      templateUrl: "app/views/quote.html"
    })
    .state("gifts", {
        url: "/gifts",
        //controller: "FeedController",
        templateUrl: "app/views/gifts.html",
      })
    .state("photo", {
      url: '/photo',
      views: {
        "Photo": { controller: "GooglePhotosController",  templateUrl: "app/views/photo.html" },
        "Empty": { controller: "GooglePhotosController",  templateUrl: "app/views/photo.html" }
      }
    }); 
      
    //$urlRouterProvider.otherwise("/");
  }

  function translationConfiguration($translateProvider) {
    $translateProvider.useStaticFilesLoader({
      prefix: '/localization/',
      suffix: '.json'
    });
    
    var supportedLanguages = ['es', 'it'];
    var firstLanguage = null;

    if (window.navigator.languages) 
    {
      //For Chrome and Mozilla
      window.navigator.languages.forEach(function (lang) {
        lang = lang.substring(0, 2);

        if (supportedLanguages.indexOf(lang) >= 0 && !firstLanguage) {
          firstLanguage = lang;
        }
      }, this);
    } 
    else if (window.navigator.language) 
    {
      // For IE
      firstLanguage = window.navigator.language.substring(0, 2);
    }
    else 
    {
      firstLanguage = 'es';
    }

    if (supportedLanguages.indexOf(firstLanguage) < 0) {
      firstLanguage = "it";
    }
    $translateProvider.preferredLanguage(firstLanguage);
  }
})();
