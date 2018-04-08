'use strict';

module.exports = function (app) {
    //importing routes
    const eventRoutes = require('./../routes/EventRoutes');
    const userRoutes = require('./../routes/UserRoutes');

    //add routes
    eventRoutes(app);
    userRoutes(app);
};