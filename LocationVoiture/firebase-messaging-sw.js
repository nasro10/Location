importScripts('https://www.gstatic.com/firebasejs/3.9.0/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/3.9.0/firebase-messaging.js');


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

