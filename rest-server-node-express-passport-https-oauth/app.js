var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');

var mongoose = require('mongoose');
//--------------add------------------
var config = require('./config');
var passport = require('passport');
//var LocalStrategy = require('passport-local').Strategy;
//add----
var authenticate = require('./authenticate');

var url = config.mongoUrl;
mongoose.connect(url);
var db = mongoose.connection;
db.on('error', console.error.bind(console, 'connection error:'));
db.once('open', function () {
    // we're connected!
    console.log("Connected correctly to server");
});



var index = require('./routes/index');
var users = require('./routes/users');

var dishRouter = require('./routes/dishRouter');
var promoRouter = require('./routes/promoRouter');
var leaderRouter = require('./routes/leaderRouter');

var app = express();

//add---------------
// Secure traffic only
app.all('*', function(req, res, next){
    console.log('req start: ',req.secure, req.hostname, req.url, app.get('port'));
  if (req.secure) {
    return next();
  };

 res.redirect('https://'+req.hostname+':'+app.get('secPort')+req.url); //这个redirect只对get有效，不管之前是post还是get，之后都会变成get
});
// Remember a request message contained as the first line, it contains the method, then the url and the HTTP version. Then you will have a header named hostname which will carry the hostname to which this request was sent. So req.hostname gives me access to the host to which this request was sent which was nothing but local host. And then the req.url gives me that url from the first line of the HTTP request message. 


// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');

// uncomment after placing your favicon in /public
//app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());

//add--------------
// passport config
//var User = require('./models/user');// So first, I will require a model called User. So, this is the model that I'm going to make use of to track my users and then store the users in my database. And then be able to manipulate them as I support the registration of new users, logging in of users, verifying the users, and so on. So that's the model that I'm going to make use of.
app.use(passport.initialize());
//passport.use(new LocalStrategy(User.authenticate()));
//passport.serializeUser(User.serializeUser());
//passport.deserializeUser(User.deserializeUser());
//被移动到authenticate.js


app.use(express.static(path.join(__dirname, 'public')));

app.use('/', index);
app.use('/users', users);


app.use('/dishes',dishRouter);
app.use('/promotions',promoRouter);
app.use('/leadership',leaderRouter);

// catch 404 and forward to error handler
app.use(function(req, res, next) {
  var err = new Error('Not Found');
  err.status = 404;
  next(err);
});

// error handler
app.use(function(err, req, res, next) {
  // set locals, only providing error in development
  res.locals.message = err.message;
  res.locals.error = req.app.get('env') === 'development' ? err : {};

  // render the error page
  res.status(err.status || 500);
  res.json({
    message: res.locals.message,
    error: res.locals.error
  });//Now you remember that our client side application side either going to be an angular application or an ionic application. And so, if you return HTML, they don't know what to do with it. Instead, if you return json strings, then in my client application I can appropriately show the error message to the user. 
});

module.exports = app;
