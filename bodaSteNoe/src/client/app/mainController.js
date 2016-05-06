function MainController($scope, $filter) {
    
  var tran = $filter('translate');
  
  //COUNTDOWN VARIABLES
	c_days = tran('DAYS');							//Countdown "Days" label
	c_hours = tran('HOURS');						//Countdown "Hours" label
	c_minutes = tran("MIN.");							//Countdown "Minutes" label
	c_seconds = tran("SEC.");							//Countdown "Seconds" label
}

angular.module('app').controller('MainController', ['$scope','$filter', MainController])