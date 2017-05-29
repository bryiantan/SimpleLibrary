var app = angular.module('TestAngularApp', ['ui.bootstrap']);
//debugger;
app.controller('modalcontroller', function ($scope, $uibModal, $window) {

    //based on size parameter the size of modal chages.
    $scope.openModal = function (title, body, okBtnText, okClickRedirect, cancelBtnText) {

        $scope.items = [title, body, okBtnText, okClickRedirect, cancelBtnText];

        var ModalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/TemplatesNg/_ModalDialogNg.html',
            controller: 'InstanceController',
            //appendTo:     //appends the modal to a element
            backdrop: false,  //disables modal closing by click on the background
            keyboard: true,     //disables modal closing by click on the ESC key
            resolve: {
                items: function () {
                    //we can send data from here to controller using resolve...
                    return $scope.items;
                }
            }
        });

        ModalInstance.result.then(function (btn) {
            if (btn == "OK") {
                $window.location.href = okClickRedirect;
            }
        }, function () { });
    };
});
app.controller('InstanceController', function ($scope, $uibModalInstance, $sce, items) {
    $scope.items = items;
    $scope.title = $scope.items[0];
    $scope.body = $scope.items[1];
    $scope.btnOkText = $scope.items[2];
    // $scope.btnOkText = $scope.items[3
    $scope.btnCancelText = $scope.items[4];
    var returnUrl = $scope.items[3];
    //allow html
    $scope.htmlBind = $sce.trustAsHtml($scope.items[1]);

    //hide or close the X close button on the top, you can write extra logic here to hide or show it
    $scope.showX = false;

    $scope.showOK = true;
    if ($scope.btnOkText == '') {
        //hide OK button
        $scope.showOK = false;
    }

    //cancel button
    $scope.showCancel = true;
    if ($scope.btnCancelText == '') {
        $scope.showCancel = false;
    }

    //OK clicked
    $scope.ok = function () {
        
        if (returnUrl == '') {
            $uibModalInstance.close('');
        } else {
            $uibModalInstance.close('OK');
        }
       
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss();
    };
});