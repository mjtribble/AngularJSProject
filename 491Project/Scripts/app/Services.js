(function () {
    angular
        .module("myApp.Services", ["ngResource"])
        .factory("myApp.Services.ItemService", ["$resource",
            function ($resource) {
                return $resource("/api/item/:itemId",
                    { itemId: '@ID' },
                    {
                        'get': { method: 'GET' },
                        'query': { method: 'GET', isArray: false,  },
                        'save': { method: 'PATCH' },
                        'remove': { method: 'DELETE' },
                    }
                );
            }
        ])
        .factory("myApp.Services.AddItemService", ["$resource",
            function ($resource) {
                return $resource("/api/item/",
                    {
                        'add': { method: 'POST' }
                    }
                );
            }
        ])
        .factory("myApp.Services.LocationService", ["$resource",
            function ($resource) {
                return $resource("/api/location/:locationId",
                    { locationId: '@ID' },
                    {
                        'add':{method : 'POST'},
                        'get': { method: 'GET' },
                        'query': { method: 'GET', isArray: false },
                        'save': { method: 'PATCH' },
                        'remove': { method: 'DELETE' },
                    }
                );
            }
        ])
        
})();