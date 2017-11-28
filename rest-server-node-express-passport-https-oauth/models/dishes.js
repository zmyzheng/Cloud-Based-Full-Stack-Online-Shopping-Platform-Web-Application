// grab the things we need
var mongoose = require('mongoose');
var Schema = mongoose.Schema;

require('mongoose-currency').loadType(mongoose);
var Currency = mongoose.Types.Currency;

var commentSchema = new Schema({
    rating:  {
        type: Number,
        min: 1,
        max: 5,
        required: true
    },
    comment:  {
        type: String,
        required: true
    },
    postedBy: {
        type: mongoose.Schema.Types.ObjectId,
        ref: 'User'
    }
    // So this will contain the object ID of the user object that created this particular comment and so that's why the second field their second property there says draft user so this is a reference to user object. Now, whenever a user inserts a comment into any particular dish, then by default I will also track the user's ID right there. Now what does this help us do? Now, if the user subsequently wants to either delete or modify the comment he or she submitted. Then we can cross check to make sure that the user is the one that actually added the comment. And so we will only allow deletion or modification of a comment that you have submitted earlier. So that way we can ensure that the correct user is given the permission to do modification. 
    //Not only this, now when we do a get request for all the dishes or a specific dish or all the comments or a specific comment we can use the mongoose populate to populate the information into the comment from the user's document. So that way we don't have to explicitly track the user information here other than a pointer to the user document. Pointer in the sense of the object ID. And then, using the populate mechanism, we can populate the information into this document and return it to the user. So that's the reason why I have introduced this additional Reference field here.
}, {
    timestamps: true
});

// create a schema
var dishSchema = new Schema({
    name: {
        type: String,
        required: true,
        unique: true
    },
    image: {
        type: String,
        required: true,
    },
    category: {
        type: String,
        required: true,
    },
    label: {
        type: String,
        default: "",
        required: true,
    },
    price: {
        type: Currency,
        required: true,
    },
    description: {
        type: String,
        required: true
    },
    comments:[commentSchema]
}, {
    timestamps: true
});

// the schema is useless so far
// we need to create a model using it
var Dishes = mongoose.model('Dish', dishSchema);

// make this available to our Node applications
module.exports = Dishes;