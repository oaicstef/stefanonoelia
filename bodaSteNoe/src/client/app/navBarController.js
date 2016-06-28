var NavBarController = (function () {
		function NavBarController($scope, $filter, $translate) {
			var _this = this;
			var tran = $filter('translate');
			this.$scope = $scope;
			this.$filter = $filter;
			this.$translate = $translate;
		}
		
		NavBarController.prototype.changeLanguage = function ($event) {
				var img = $event.currentTarget;
				if (img.id == 'IT-Flag') {
					this.$translate.use('it');
				} else {
					this.$translate.use('es')
				}
	    };
		return NavBarController;
	} ());

angular.module('app').controller('NavBarController', ['$scope', '$filter', '$translate', NavBarController])