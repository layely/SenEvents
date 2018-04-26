'use strict';
var mongoose = require('mongoose'),
    Event = mongoose.model('Event');

exports.listAll = function (req, res) {
    const queryConditions = {};

    Event.find(queryConditions).lean().exec(function (err, events) {
        if (err) {
            res.send(err);
        }
        events.forEach(event => {
            event.Id = event._id
        });
        res.json(events);
    });
};

exports.addOne = function (req, res) {
    var newEvent = new Event(req.body);

    newEvent.save(function (err, event) {
        if (err) {
            res.send(err);
        }
        res.json(event);
    });
};

exports.getOne = function (req, res) {
    const queryConditions = {
        _id: req.params._id
    };

    Event.findOne(queryConditions).lean().exec(function (err, event) {
        if (err) {
            res.send(err);
        }
        event.Id = event._id;
        res.json(event);
    });
};

exports.modifyOne = function (req, res) {
    const queryConditions = {
        _id: req.params._id,
    };

    const modified = req.body;
    Event.update(queryConditions, modified, function (err, result) {
        if (err) {
            res.send(err);
        }
        res.json(result);
    });
};

exports.deleteOne = function (req, res) {
    const queryConditions = {
        _id: req.params._id
    };

    Event.remove(queryConditions, function (err, result) {
        if (err) {
            res.end(err);
        }
        res.json(result);
    });
};