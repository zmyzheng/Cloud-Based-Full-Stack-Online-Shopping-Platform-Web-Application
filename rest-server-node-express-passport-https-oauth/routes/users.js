var express = require('express');
var router = express.Router();
var passport = require('passport');
var User = require('../models/user');
var Verify = require('./verify');//This verify module encapsulates every thing related to managing the JSON Web tokens and also verifying the user's identities. 

/* GET users listing. */
router.get('/', Verify.verifyOrdinaryUser, Verify.verifyAdmin, function(req, res, next) {//这些传递进来的函数其实都是中间件，依次执行
  User.find({}, function (err, users) {
        if (err) throw err;
        res.json(users);
  })
    
});

router.post('/register', function(req, res) {
    User.register(new User({ username : req.body.username }),//这里之所以不包括password是因为服务器端不存未加密的password，下面传入后自动加密为salt和hash
      req.body.password, function(err, user) {
        if (err) {
            return res.status(500).json({err: err});
        }
        //add-------------
        //非必填项可以这样添加
        if(req.body.firstname) {
            user.firstname = req.body.firstname;
        }
        if(req.body.lastname) {
            user.lastname = req.body.lastname;
        }
        //对数据库的操作都要保存
        user.save(function(err,user) {
            passport.authenticate('local')(req, res, function () {
                return res.status(200).json({status: 'Registration Successful!'});
            });
        });
    });
});

router.post('/login', function(req, res, next) {
  passport.authenticate('local', function(err, user, info) {
    if (err) {
      return next(err);
    }
    if (!user) {
      return res.status(401).json({
        err: info
      });
    }
    req.logIn(user, function(err) {
      if (err) {
        return res.status(500).json({
          err: 'Could not log in user'
        });
      }//passport itself makes available two methods on the request called as login and logout and I'm going to make use of them to help login the user. So when I say req login and then supply the user here, then it'll try to log in that user. So in here, the first part, if the user exists, the user information will be retrieved from the MongoDB database and made available to us. And so that user information will be passed in to the login function which is where you're going to try and login to passport. So this is passport's way of doing things. 
        
      var token = Verify.getToken(user);
      res.status(200).json({
        status: 'Login successful!',
        success: true,
        token: token
      });//passport itself makes available two methods on the request called as login and logout and I'm going to make use of them to help login the user. So when I say req login and then supply the user here, then it'll try to log in that user. So in here, the first part, if the user exists, the user information will be retrieved from the MongoDB database and made available to us. And so that user information will be passed in to the login function which is where you're going to try and login to passport. So this is passport's way of doing things. 
    });
  })(req,res,next);
});

router.get('/logout', function(req, res) {
    req.logout();
  res.status(200).json({
    status: 'Bye!'
      //. At this point, I should also destroy the token so that the user can no longer access my server. I don't do that specifically in the code here, but that should also be done. 
  });
});


//add----
router.get('/facebook', passport.authenticate('facebook'),
  function(req, res){});
// When the user calls /users/facebook URI, then I'm going to call passport authenticate with Facebook as the strategy. So when that is called, then this will result in the user being sent to Facebook for user authentication. So that route is set up. Now we saw that when we set up the Facebook authentication strategy, we supplied a callback URL and that callback URL was localhost3443/users/facebook/callback. Now that facebook/callback URI will now be setup with this router get here.
router.get('/facebook/callback', function(req,res,next){
  passport.authenticate('facebook', function(err, user, info) {
    if (err) {
      return next(err);
    }
    if (!user) {
      return res.status(401).json({
        err: info
      });
    }
      //下边的就和普通login相同了
    req.logIn(user, function(err) {
      if (err) {
        return res.status(500).json({
          err: 'Could not log in user'
        });
      }
      var token = Verify.getToken(user);
      res.status(200).json({
        status: 'Login successful!',
        success: true,
        token: token
      });
    });
  })(req,res,next);
});

module.exports = router;