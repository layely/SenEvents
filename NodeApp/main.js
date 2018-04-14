'use strict';
const express = require('express'),
    app = express(),
    port = process.env.PORT || 8000;

const bodyParser = require('body-parser');
const mongoose = require('mongoose');
mongoose.set('debug', 'true');

const fileUpload = require('express-fileupload');
app.use(fileUpload({ safeFileNames: true, preserveExtension: 4 }));

//Load database configuration
const db = require('./config/db');

//Load created models
const loadModels = require('./app/helpers/ModelLoader');
loadModels();

//Create a database connection 
mongoose.Promise = global.Promise;
mongoose.connect(db.url);

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

//add middlewares
const middlewares = require('./app/middleware/index.js');
app.use(middlewares);

//add routes
const importAndLoadRoutes = require('./app/helpers/setUpRoutes');
importAndLoadRoutes(app);

app.listen(port);

console.log('Cahier de Texte RESTful API server started on: ' + port);