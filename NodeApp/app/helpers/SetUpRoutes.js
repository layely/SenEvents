'use strict';

module.exports = function (app) {
    //importing routes
    const eventRoutes = require('./../routes/EventRoutes');
    const userRoutes = require('./../routes/UserRoutes');
    const imageRoutes = require('./../routes/ImageRoutes');

    //add routes
    eventRoutes(app);
    userRoutes(app);
    imageRoutes(app);

};