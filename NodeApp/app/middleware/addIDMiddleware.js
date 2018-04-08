'use strict';
/*
    Ce 'middleware' permet de générer un id pour un nouveau objet à ajouter 
    (method == Post) à la base de données.
 */
module.exports = function (req) {
    if (!req.body._id || req.body._id == '0') {
        const id = Date.now();
        console.log('generated _id: ', id);
        req.body._id = id;
    }
};