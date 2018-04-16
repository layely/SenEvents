"use strict";
const path = require('path');
const uuidv1 = require('uuid/v1');

exports.upload = function (req, res) {
    console.log("inside upload");
    if (!req.files)
        return res.status(400).send('No files were uploaded.');

    // The name of the input field (i.e. "sampleFile") is used to retrieve the uploaded file
    let image = req.files.image;
    const uuid = uuidv1();

    const fileId = uuid + path.extname(image.name);
    // File final location
    const fileLocation = path.join(path.resolve('./upload'), "/", fileId);

    // Use the mv() method to place the file somewhere on your server
    image.mv(fileLocation, function (err) {
        if (err)
            return res.status(500).send(err);

        res.send(fileId);
    });
};

exports.download = function (req, res) {
    const id = req.params._id;
    const filePath = path.join(path.resolve('./'), '/upload', id);

    res.sendFile(filePath);
};