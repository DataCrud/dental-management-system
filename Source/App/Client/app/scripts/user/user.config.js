﻿angular.module("dentalApp")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function ($urlRouterProvider, $stateProvider) {
            "use strict";

            $stateProvider
                .state("root.user", {
                    url: "/user",
                    views: {
                        "" : {
                            templateUrl: "app/views/user/user.tpl.html",
                            controller: "UserController"
                        }
                    }
                });
        }
    ]);