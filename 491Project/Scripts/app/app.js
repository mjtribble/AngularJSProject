angular.module('myApp', [])
    .controller('listController', ['$scope', 
        function ($scope) {
            $scope.locations = [
                {id: 0,label: "Refrigerator"},
                {id: 1,label: "Outdoor Fridge"},
                {id: 2,label: "Cabinet A"},
                {id: 3,label: "Pantry"}
            ];
            $scope.items = [
                {id: 1, name: "Milk", description: "Cow juice", quantity: 1, expires: "2.26.17 7:00AM", location: "Refrigerator"},
                {id: 1, name: "Milk", description: "Cow juice", quantity: 1, expires: "2.26.17 7:00AM", location: "Refrigerator"},
                {id: 1, name: "Milk", description: "Cow juice", quantity: 1, expires: "2.26.17 7:00AM", location: "Refrigerator"},
                {id: 1, name: "Milk", description: "Cow juice", quantity: 1, expires: "2.26.17 7:00AM", location: "Refrigerator"}
         ];
        }]);

//$scope.$watch("filter.One + filter.Two",
//    function (newVal, oldVal) {
//        if (newVal == "FooBar") {
//            alert('Filters are set to foobar')
//        }
//    });

