// var App;
// (function (App) {
//     var Common;
//     (function (Common) {
//         var ApiService = (function () {
//             function ApiService($http, apiRoutes, apiDomain) {
//                 this.$http = $http;
//                 this.apiRoutes = apiRoutes;
//                 this.apiDomain = apiDomain;
//                 this.REQUIRED_VALID_PARAMS = "Every parameter has to have a value different than empty string.";
//                 delete $http.defaults.headers.common['X-Requested-With'];
//             }
//             ApiService.prototype.route = function (routeName, param) {
//                 var mapedRoute = this.apiRoutes[routeName];
//                 if (mapedRoute === undefined)
//                     throw "Unknown route: " + routeName;
//                 if (param !== undefined) {
//                     mapedRoute += '/' + param;
//                 }
//                 return this.apiDomain + mapedRoute;
//             };
//             ApiService.prototype.routeCustomParam = function (routeName, paramName, paramValue) {
//                 if (Common.Utils.isUndefinedOrEmpty(routeName) || Common.Utils.isUndefinedOrEmpty(paramName) || Common.Utils.isUndefinedOrEmpty(paramValue)) {
//                     throw this.REQUIRED_VALID_PARAMS;
//                 }
//                 return this.route(routeName) + '/' + paramName + '/' + paramValue;
//             };
//             ApiService.prototype.routeCustom = function (routeName, customizedString) {
//                 if (Common.Utils.isUndefinedOrEmpty(routeName) || Common.Utils.isUndefinedOrEmpty(customizedString)) {
//                     throw this.REQUIRED_VALID_PARAMS;
//                 }
//                 return this.route(routeName) + customizedString;
//             };
//             ApiService.prototype.get = function (url) {
//                 if (url === undefined) {
//                     throw "Undefined url";
//                 }
//                 var promise = this.$http.get(url)
//                     .error(function (data, status) {
//                     console.error('ApiService.Get Error:', status, data);
//                 })
//                     .catch(function (response) {
//                     console.error('ApiService.Get Catch error', response);
//                 });
//                 return promise;
//             };
//             ApiService.prototype.post = function (url, dataObject) {
//                 if (url === undefined) {
//                     throw "Undefined url";
//                 }
//                 if (dataObject === undefined) {
//                     throw "Undefined dataObject";
//                 }
//                 return this.$http.post(url, dataObject);
//             };
//             ApiService.prototype.put = function (url, dataObject) {
//                 if (url === undefined) {
//                     throw "Undefined url";
//                 }
//                 if (dataObject === undefined) {
//                     throw "Undefined dataObject";
//                 }
//                 return this.$http.put(url, dataObject);
//             };
//             ApiService.prototype.delete = function (url) {
//                 if (url === undefined) {
//                     throw "Undefined url";
//                 }
//                 return this.$http.delete(url);
//             };
//             ApiService.$inject = ['$http', 'apiRoutes', 'apiDomain'];
//             return ApiService;
//         }());
//         Common.ApiService = ApiService;
//         App.module.register.service("ApiService", ApiService);
//     })(Common = App.Common || (App.Common = {}));
// })(App || (App = {}));
