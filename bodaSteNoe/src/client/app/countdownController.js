var CountDownController = (function () {
    function CountDownController($scope,$interval) {

        var _this = this;
        this.$scope = $scope;
        _this.days = 0;
        _this.hours = 0;
        _this.minutes = 0;
        _this.seconds = 0;
          
        var $tis = this,
            future = new Date("2016/08/19 4:30 PM"),
            counter;
        
        // $parent.html('<div class="days"><span>' + $tis.c_days + '</span><div></div></div>' +
        //     '<div class="hours"><span>' + $tis.c_hours + '</span><div></div></div>' +
        //     '<div class="minutes"><span>' + $tis.c_minutes + '</span><div></div></div>' +
        //     '<div class="seconds"><span>' + $tis.c_seconds + '</span><div></div></div>');

        //counter = setInterval(changeTime(_this, future), 1000);
        var time = $interval(changeTime, 1000, null, null, _this, future);
    }
    
    function changeTime (scope, future) {
        var today = new Date(),
            _dd = future - today,
            $parent = $(".countdown");

        if (_dd < 0) {
            $parent.html('<div class="end">' + countdownEndMsg + '</div>');
            clearInterval(counter);

            return false;
        }

        var days = Math.floor(_dd / (60 * 60 * 1000 * 24) * 1);
        var hours = Math.floor((_dd % (60 * 60 * 1000 * 24)) / (60 * 60 * 1000) * 1);
        var minutes = Math.floor(((_dd % (60 * 60 * 1000 * 24)) % (60 * 60 * 1000)) / (60 * 1000) * 1);
        var seconds = Math.floor((((_dd % (60 * 60 * 1000 * 24)) % (60 * 60 * 1000)) % (60 * 1000)) / 1000 * 1);

        scope.days =days;
        scope.hours = hours;
        scope.minutes = minutes;
        scope.seconds = seconds;
    };

    return CountDownController;
} ());

angular.module('app').controller('CountDownController', ['$scope', '$interval', CountDownController]);

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