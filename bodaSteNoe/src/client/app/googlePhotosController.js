var GooglePhotosController = (function () {
    function GooglePhotosController($scope, $http, $state) {
        var vm = this;
        vm.photos = null;
        vm.fileName = null;
        vm.googleAlbum = googlePhotosAlbum;
        vm.GoogleUrl = googlePhotosAlbum;
        vm.InstagramUrl = instagramAlbum;

        if ($state.is("photo")){
            $('#home').removeClass('divider-bottom-1');
        }

        $http({
            method: 'GET',
            url: apiUrl + 'api/google/photos'
        }).then(function successCallback(response) {
            vm.photos = response.data.Photos;
        }, function errorCallback(response) {
            console.log(response);
        });

        $scope.uploadPhoto = function (input) {
            vm.FileUploaded = input.files.length;

            $.each(input.files, function(idx, item)
                {
                  var fd = new FormData();
                  fd.append('file', item);
                  $http({
                     method: 'POST',
                     url: apiUrl + 'api/google',
                     transformRequest: angular.identity,
                     headers: {'Content-Type': undefined},
                     data:  fd
                  }).then(function successCallback(response) {
                          vm.FileUploaded += item.name + " has been uploaded. ";   
                      }, function error(response) {
                          alert("Error");
                      })
                });
        };

        $scope.capturePhoto = function (e) 
        {
            e.preventDefault();
            angular.element('#capturePhoto').trigger('click');
        };
        
        $scope.triggerUploadPhoto = function (e) 
        {
            e.preventDefault();
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
    GooglePhotosController.$inject = ['$scope', '$http', '$state'];
    return GooglePhotosController;
} ());

angular.module('app').controller('GooglePhotosController', GooglePhotosController);