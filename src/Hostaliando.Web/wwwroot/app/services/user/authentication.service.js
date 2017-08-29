(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('authenticationService', authenticationService);

    authenticationService.$inject = [
        '$q',
        '$http',
        'httpService',
        'routingService',
        'sessionService'];

    function authenticationService(
        $q,
        $http,
        http,
        routingService,
        sessionService) {

        var baseUrl = 'api/v1/auth/';

        var service = {
            get: get,
            post: post
        };

        return service;

        function post(model) {
            var deferred = $q.defer();


            var dataToSend = {
                username: model.email,
                password: model.password,
                grant_type: 'password'
            };

            $http({
                method: 'POST',
                url: baseUrl,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                data: $.param(dataToSend)
            }).then(postCompleted)
              .catch(postError);

            return deferred.promise;

            function postCompleted(response) {
                setSessionUser(deferred, response.data);
                //deferred.resolve(response.data);
            }

            function postError(response) {
                deferred.reject(response.data);
            }
        }

        function get() {
            return http.get(baseUrl + 'current');
        }

        function setSessionUser(deferred, responseToken) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + responseToken.access_token;

            get()
                .then(getSessionCompleted)
                .catch(getSessionError);

            function getSessionCompleted(userResponse) {
                var user = {
                    email: userResponse.email,
                    id: userResponse.id,
                    name: userResponse.name,
                    token: responseToken.access_token,
                    hostel: userResponse.hostel
                };

                sessionService.setCurrentUser(user);
                deferred.resolve(userResponse);
            }

            function getSessionError(userReponse) {
                alert('Error al cargar la sesion');
                deferred.reject(userReponse);
            }
        }
    }
})();