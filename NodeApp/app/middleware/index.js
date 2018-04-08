'use strict';
module.exports = function (req, res, next) {
    if (req.method == 'POST') {
        const addID = require('./addIDMiddleware');
        addID(req);
    }

    next();
};