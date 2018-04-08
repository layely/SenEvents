'use strict';
module.exports = function (app) {
    var userController = require('./../controllers/UserController');

    app.route('/users')
        .get(userController.listAll)
        .post(userController.addOne);

    app.route('/user/:_id')
        .get(userController.getOne)
        .put(userController.modifyOne)
        .delete(userController.deleteOne);
};