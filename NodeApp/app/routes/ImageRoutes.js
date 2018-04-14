"use strict";

module.exports = function (app) {
    var imageController = require('./../controllers/ImageLoadController');

    app.route('/images/:_id')
        .get(imageController.download);

    app.route('/images')
        .post(imageController.upload);
};