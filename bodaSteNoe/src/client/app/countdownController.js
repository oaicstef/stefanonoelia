var CountDownController = (function () {
    function CountDownController($scope,$interval, $http, $compile, $state) {

        var _this = this;
        this.$scope = $scope;
        _this.days = 0;
        _this.hours = 0;
        _this.minutes = 0;
        _this.seconds = 0;
        
        var $tis = this,
            future = new Date("2016/08/19 0:00 AM"),
            counter;
        
        _this.waitTime = 1000;
        
        if ($interval){
            _this.refreshInterval = $interval(changeTime, 30, null, null, _this, future, $http, $compile, $state, $scope, $interval);
        }
    }
    
    function changeTime (scope, future, $http, $compile, $state, $scope, $interval) {
        var today = new Date(),
            _dd = future - today,
            $parent = $(".countdown");

        if (_dd < 0) {
            if (scope.waitTime <= 1000)
            {
                $('#home').removeClass('divider-bottom-1');
                $interval.cancel(scope.refreshInterval);
                var title = $('.hero-text');
                title.html("<div>{{ 'StartWedding' | translate }}</div><div>{{ 'SharePictures' | translate }}</div>");
                var compiled = $compile(title.html())($scope);
                title.html(compiled);
                $parent.hide();
                $state.go("photo");
                scope.waitTime = 999999999999999999999;
            }

            return false;
        }

        var days = Math.floor(_dd / (60 * 60 * 1000 * 24) * 1);
        var hours = Math.floor((_dd % (60 * 60 * 1000 * 24)) / (60 * 60 * 1000) * 1);
        var minutes = Math.floor(((_dd % (60 * 60 * 1000 * 24)) % (60 * 60 * 1000)) / (60 * 1000) * 1);
        var seconds = Math.floor((((_dd % (60 * 60 * 1000 * 24)) % (60 * 60 * 1000)) % (60 * 1000)) / 1000 * 1);

        scope.days = days;
        scope.hours = hours;
        scope.minutes = minutes;
        scope.seconds = seconds;
    };

    return CountDownController;
} ());

angular.module('app').controller('CountDownController', ['$scope', '$interval','$http','$compile','$state', CountDownController]);

(function () {
    angular.module('app').directive('countdown', [
        'Util',
        '$interval',
        function (Util, $interval) {
            return {
                restrict: 'A',
                scope: { date: '@' },
                link: function (scope, element) {
                    var future;
                    future = new Date(scope.date);
                    $interval(function () {
                        var diff;
                        diff = Math.floor((future.getTime() - new Date().getTime()) / 1000);
                        return element.text(Util.dhms(diff));
                    }, 1000);
                }
            };
        }
    ]).factory('Util', [function () {
            return {
                dhms: function (t) {
                    var days, hours, minutes, seconds;
                    days = Math.floor(t / 86400);
                    t -= days * 86400;
                    hours = Math.floor(t / 3600) % 24;
                    t -= hours * 3600;
                    minutes = Math.floor(t / 60) % 60;
                    t -= minutes * 60;
                    seconds = t % 60;
                    return [
                        days + 'd',
                        hours + 'h',
                        minutes + 'm',
                        seconds + 's'
                    ].join(' ');
                }
            };
        }]);
}.call(this));