var express = require('express');

var bodyParser = require('body-parser');

//-----------add--
var mongoose = require('mongoose');

var Promotions = require('../models/promotions');
//----------------


var promoRouter = express.Router();

promoRouter.use(bodyParser.json());

promoRouter.route('/')
.get(function(req,res,next){
    
    Promotions.find({}, function (err, promotion) {
        if (err) throw err;
        res.json(promotion);
        // So this res.json basically a method on the response message that we are going to send back. When you call the json method and supply a. JavaScript object or a JavaScript object array in here as a parameter is going to convert that into adjacent string and then ship it back to the client from this server. That's it in one single statement, you have completed everything. You don't even need to set the headers. Because headers will be automatically set to the status code will be set 200 and the content type will be set to application/json automatically when you call this method. 
    });
})

.post(function(req, res, next){
    
    Promotions.create(req.body, function (err, promotion) {
        // So in here Dishes.create has the first parameter. I'm going to simply take the requests body. Remember that the requests body has already been parsed by the body parser and converted into appropriate JSON and hung on to that body property there. So I'm just going to parse that to my MongoDB server and say put this into the database. So that's why the first parameter is the item to be inserted. So all I need to do is say req.body. 
        if (err) throw err;
        console.log('promotion created!');
        var id = promotion._id;

        res.writeHead(200, {
            'Content-Type': 'text/plain'
        });
        res.end('Added the promotion with id: ' + id);
    });
})

.delete(function(req, res, next){
        
    Promotions.remove({}, function (err, resp) {
        if (err) throw err;
        res.json(resp);
    });
});


promoRouter.route('/:promotionId')

.get(function(req,res,next){
    
    Promotions.findById(req.params.promotionId, function (err, promotion) {
        if (err) throw err;
        res.json(promotion);
    });
})

.put(function(req, res, next){

    Promotions.findByIdAndUpdate(req.params.promotionId, {
        $set: req.body
    }, {
        new: true
    }, function (err, promotion) {
        if (err) throw err;
        res.json(promotion);
    });
})

.delete(function(req, res, next){

     Promotions.findByIdAndRemove(req.params.promotionId, function (err, resp) {        
        if (err) throw err;
        res.json(resp);
    });
});


module.exports = promoRouter;