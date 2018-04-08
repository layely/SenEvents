'use strict';
var mongoose = require('mongoose'),
    User = mongoose.model('User');

exports.listAll = function (req, res) {
    const queryConditions = {};

    User.find(queryConditions, function (err, user) {
        if (err) {
            res.send(err);
        }
        res.json(user);
    });
};

exports.addOne = function (req, res) {
    var newUser = new User(req.body);

    newUser.save(function (err, user) {
        if (err) {
            res.send(err);
        }
        res.json(user);
    });
};

exports.getOne = function (req, res) {
    const queryConditions = {
        _id: req.params._id
    };

    User.findOne(queryConditions, function (err, user) {
        if (err) {
            res.send(err);
        }
        res.json(user);
    });
};

exports.modifyOne = function (req, res) {
    const queryConditions = {
        _id: req.params._id,
    };

    const modified = req.body;
    User.update(queryConditions, modified, function (err, result) {
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

    User.remove(queryConditions, function (err, result) {
        if (err) {
            res.end(err);
        }
        res.json(result);
    });
};