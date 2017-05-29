
var app = angular.module('TestAngularApp', ['ui.bootstrap']);
//http://techiesweb.net/2014/02/16/asp-net-mvc-request-isajaxrequest-method-returns-false-for-angular-http-service.html
app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
}]);

//debugger;
app.controller('modalcontroller', function ($scope, $uibModal) {
   
    $scope.animationsEnabled = true;

    //based on size parameter the size of modal chages.
    $scope.openModal = function (size) {

        $scope.items = [$scope.antiForgeryToken];

        var ModalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'myModal.html',
            controller: 'InstanceController',
            backdrop: false,
            resolve: {
                items: function () {
                    return $scope.items;
                }
            }
        });

        ModalInstance.result.then(function (selecedItem) {
            //do something on OK / Cancel button click
        }, function ()
        {
        });
    };
});
app.controller('InstanceController', function ($scope, $uibModalInstance, $http, $window, items) {

    $scope.items = items;

    $scope.ok = function () {
        
        var Indata = { rURL: 'dummyTestParam' };
        $http({
            method: 'POST',
            url: '/home/AcceptPolicy',
            params: Indata,
            headers: {
                'RequestVerificationToken': $scope.items[0]
            }
        }).then(function successCallback(response) {
            $scope.status = response.status;
            $scope.data = response.data;
            $window.location.href = response.data;
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });

        $uibModalInstance.close();
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss('cancel');
    };
});