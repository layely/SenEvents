'use strict';
var mongoose = require('mongoose'),
    Cours = mongoose.model('Event');

exports.listAll = function (req, res) {
    const queryConditions = {};

    Cours.find(queryConditions, function (err, cours) {
        if (err) {
            res.send(err);
        }
        res.json(cours);
    });
};

exports.addOne = function (req, res) {
    var newCours = new Cours(req.body);

    newCours.save(function (err, cours) {
        if (err) {
            res.send(err);
        }
        res.json(cours);
    });
};

exports.getOne = function (req, res) {
    const queryConditions = {
        _id: req.params._id
    };

    Cours.findOne(queryConditions, function (err, cours) {
        if (err) {
            res.send(err);
        }
        res.json(cours);
    });
};

exports.modifyOne = function (req, res) {
    const queryConditions = {
        _id: req.params._id,
    };

    const modified = req.body;
    Cours.update(queryConditions, modified, function (err, result) {
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

    Cours.remove(queryConditions, function (err, result) {
        if (err) {
            res.end(err);
        }
        res.json(result);
    });
};