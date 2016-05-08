var MainController = (function () {
		function MainController($scope, $filter, $translate) {
			var _this = this;
			var tran = $filter('translate');
			this.$scope = $scope;
			this.$filter = $filter;
			this.$translate = $translate;
			
			//COUNTDOWN VARIABLES
			this.$translate("DAYS").then(function (data) {
				c_days = data;	
			});			//Countdown "Days" label
			c_hours = tran('HOURS');						//Countdown "Hours" label
			c_minutes = tran("MIN.");							//Countdown "Minutes" label
			c_seconds = tran("SEC.");							//Countdown "Seconds" label
		}
		
		MainController.prototype.changeLanguage = function ($event) {
				var img = $event.currentTarget;
				if (img.id == 'IT-Flag') {
					this.$translate.use('it');
				} else {
					this.$translate.use('es')
				}
		};
		return MainController;
	} ());

angular.module('app').controller('MainController', ['$scope', '$filter', '$translate', MainController])