var mongoose = require('mongoose');
var Schema = mongoose.Schema;
var passportLocalMongoose = require('passport-local-mongoose');//The additional thing that you would see here is that I am saying var passportLocalMongoose and then require passport-local-mongoose. This is a mongoose support plugin that I'm going to use together with my user model here. Now this provides a lot of interesting methods that my passport-local requires in order to support the user model. So all that is supported by passportLocalMongoose and then the complexity of using the passport local package becomes more straightforward after this

var User = new Schema({
    username: String,
    password: String,
    //add----
    OauthId: String,
    OauthToken: String,
    firstname: {
      type: String,
        default: ''
    },
    //add---------------
    lastname: {
      type: String,
        default: ''
    },
    admin:   {
        type: Boolean,
        default: false
    }
});

//add-----------
User.methods.getName = function() {
    return (this.firstname + ' ' + this.lastname);
};

User.plugin(passportLocalMongoose);

module.exports = mongoose.model('User', User);