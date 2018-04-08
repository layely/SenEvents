'use strict';
const mongoose = require('mongoose');
var idvalidator = require('mongoose-id-validator');
const Schema = mongoose.Schema;

var eventSchema = new Schema({
    _id: { type: Number, required: true },
    Title: { type: String, required: true },
    PhotoUri: {type: String, required: false},
    StartDate: { type: String, required: true }, // Format: dd/mm/yyyy-hh:mm
    Text: { type: String, required: true },
    Price: { type: Number, required: true },
    Category: {type: String, required: true},
    City: {type: String, required: true},
    PublisherEmail: {type: String, required: true}
});

eventSchema.plugin(idvalidator);
module.exports = mongoose.model('Event', eventSchema);