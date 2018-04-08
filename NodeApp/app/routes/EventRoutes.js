'use strict';
module.exports = function (app) {
    var eventController = require('./../controllers/EventController');

    app.route('/events')
        .get(eventController.listAll)
        .post(eventController.addOne);

    app.route('/event/:_id')
        .get(eventController.getOne)
        .put(eventController.modifyOne)
        .delete(eventController.deleteOne);
};