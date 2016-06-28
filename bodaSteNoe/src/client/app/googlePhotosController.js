var GooglePhotosController = (function () {
    function GooglePhotosController($scope, $http) {
        var vm = this;
        vm.photos = null;
        vm.fileName = null;
        vm.googleAlbum = googlePhotosAlbum;

        $http({
            method: 'GET',
            url: apiUrl + 'api/google/photos'
        }).then(function successCallback(response) {
            vm.photos = response.data.Photos;
        }, function errorCallback(response) {
            console.log(response);
        });

        $scope.uploadPhoto = function (input) {
            var reader = new FileReader();
            reader.onloadend = function loaded(params) {
                var d = this.result;
                var fd = new FormData();
                fd.append('file', input.files[0]);
                $http({
                   method: 'POST',
                   url: apiUrl + 'api/google',
                   transformRequest: angular.identity,
                   headers: {'Content-Type': undefined},
                   data:  fd
                }).then(function successCallback(response) {
                        alert("ok uploaded");   
                    }, function error(response) {
                        alert("Error");
                    })
            };
            
            reader.readAsBinaryString(input.files[0]);
        };

        $scope.capturePhoto = function () 
        {
            //e.preventDefault();
            angular.element('#capturePhoto').trigger('click');
        };
        $scope.triggerUploadPhoto = function () 
        {
            //e.preventDefault();
            angular.element('#browsePhoto').trigger('click');
        };
    }

    GooglePhotosController.prototype.capturePhoto = function (e) 
    {
        e.preventDefault();
        angular.element('#browsePhoto').trigger('click');
    };
    GooglePhotosController.prototype.triggerUploadPhoto = function (e) 
    {
        e.preventDefault();
        angular.element('#uploadPhoto').trigger('click');
    };
    GooglePhotosController.$inject = ['$scope', '$http'];
    return GooglePhotosController;
} ());

angular.module('app').controller('GooglePhotosController', GooglePhotosController);