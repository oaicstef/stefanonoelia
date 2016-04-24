function TestController($scope) {
    var data = { "Id": 3, "FirstName": "Test", "LastName": "User", "Username": "testuser", "IsApproved": true, "IsOnlineNow": true, "IsChecked": true };
    $http.post(
        '/api/values',
        JSON.stringify(data),
        {
            headers: {
                'Content-Type': 'application/json'
            }
        }
    ).success(function (data) {
        $scope.person = data;
    });
}

angular.module('app').controller('TestController', ['$scope']);