var GooglePhotosController = (function () {
    function GooglePhotosController($scope, $http) {
        var vm = this;
        vm.photos  = null;
        
        $http({
            method: 'GET',
            url: apiUrl + 'api/google/photos'
        }).then(function successCallback(response){
            vm.photos = response.data.Photos;
        }, function errorCallback(response){
            console.error(response.data);
        });
    }
    
    GooglePhotosController.$inject = ['$scope','$http'];
    return GooglePhotosController;
}());

angular.module('app').controller('GooglePhotosController', GooglePhotosController);