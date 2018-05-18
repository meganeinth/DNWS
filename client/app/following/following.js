'use strict';

angular.module('followingList', ['ngRoute'])
  .component('followingList', {
    templateUrl: 'following/following.html',
    controller: ['$http', '$rootScope', function TweetListController($http, $rootScope) {
        var self = this;

        

      const requestOptions = {
          headers: { 'X-session': $rootScope.x_session }
      };

      $http.get('http://localhost:8080/twitterapi/following/', requestOptions).then(function (response) {
          self.followings = response.data;
          
      });
        //example base on  home.js
        //give me some point
        //
      $http.post('http://localhost:8080/twitterapi/follow/', requestOptions).then(function (response) {
          self.follow = response.data;
          self.follow.forEach(function iterator(value, index, collection) {
              value.follow = decodeURIComponent(value.follow);
          });
      });

      $http.delete('http://localhost:8080/twitterapi/unfollow/', requestOptions).then(function (response) {
          self.unfollow = response.data;
          self.unfollow.forEach(function iterator(value, index, collection) {
              value.unfollow = decodeURIComponent(value.unfollow);
          });
      });

        //

    }]
});