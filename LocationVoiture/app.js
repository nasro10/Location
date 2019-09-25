  
  // Initialize Firebase
  var config = {
    apiKey: "AIzaSyBpOK-2o5Cs9lrdVO7_nYjFFsAhLGuVASI",
    authDomain: "projectid.firebaseapp.com",
    databaseURL: "https://projectid.firebaseio.com",
    projectId: "projectid",
    storageBucket: "projectid.appspot.com",
    messagingSenderId: "620614048675"
  };
  firebase.initializeApp(config);
  
  const messaging = firebase.messaging();
  messaging.requestPermission().then(function() {
     //getToken(messaging);
     return messaging.getToken();
  }).then(function(token){
      console.log(token);
     

      })
  })
.catch(function(err) {
  console.log('Permission denied', err);
});


messaging.onMessage(function(payload){
console.log('onMessage: ',payload);
});
