'use strict';
const mongoose = require('mongoose');
var idvalidator = require('mongoose-id-validator');
const Schema = mongoose.Schema;

var userSchema = new Schema({
    _id: { type: Number, required: true },
    PhotoProfileId: {type: String, required: false},
    Name: { type: String, required: true },
    Email: {type: String, required: true},
    Password: {type: String, require: true},
    City: { type: String, required: true }
});

userSchema.plugin(idvalidator);
module.exports = mongoose.model('User', userSchema);