const { task, src, dest, series, parallel } = require("gulp");
const concat = require("gulp-concat");
const csso = require("gulp-csso");
const eslint = require("gulp-eslint");
const htmlmin = require("gulp-htmlmin");
const rename = require("gulp-rename");
const terser = require("gulp-terser");

/**
 * 
 * Tasks of build from library and generate public files.
 * 
 */
task("lib/exceljs", () => {
  return src("node_modules/exceljs/dist/exceljs.min.js")
    .pipe(dest("public/lib/exceljs/dist/"));
});
task("lib/firebase", () => {
  return src(["node_modules/firebase/firebase-app.js",
    "node_modules/firebase/firebase-auth.js",
    "node_modules/firebase/firebase-database.js"])
    .pipe(dest("public/lib/firebase/"));
});
task("lib/material-components-web", () => {
  return src(["node_modules/material-components-web/dist/material-components-web.min.css",
    "node_modules/material-components-web/dist/material-components-web.min.js"])
    .pipe(dest("public/lib/material-components-web/dist/"));
});
task("lib/material-design-icons", () => {
  return src(["node_modules/material-design-icons/iconfont/MaterialIcons-Regular.eot",
    "node_modules/material-design-icons/iconfont/MaterialIcons-Regular.ijmap",
    "node_modules/material-design-icons/iconfont/MaterialIcons-Regular.svg",
    "node_modules/material-design-icons/iconfont/MaterialIcons-Regular.ttf",
    "node_modules/material-design-icons/iconfont/MaterialIcons-Regular.woff",
    "node_modules/material-design-icons/iconfont/MaterialIcons-Regular.woff2"])
    .pipe(dest("public/lib/material-design-icons/iconfont/"));
});
task("lib/material-icons", () => {
  return src("node_modules/material-design-icons/iconfont/material-icons.css")
   .pipe(csso())
   .pipe(rename("material-icons.min.css"))
   .pipe(dest("public/lib/material-design-icons/iconfont/"));
});
task("lib/moment", () => {
  return src("node_modules/moment/min/moment.min.js")
    .pipe(dest("public/lib/moment/"));
});
task("lib/moment", () => {
  return src("node_modules/moment/min/moment.min.js")
    .pipe(dest("public/lib/moment/"));
});
task("lib/moment-duration-format", () => {
  return src("node_modules/moment-duration-format/lib/moment-duration-format.js")
    .pipe(rename("moment-duration-format.min.js"))
    .pipe(terser())
    .pipe(dest("public/lib/moment-duration-format/"));
});
task("lib/polyfill", () => {
  return src("node_modules/@babel/polyfill/dist/polyfill.min.js")
    .pipe(dest("public/lib/polyfill/dist/"));
});

/**
 * 
 * Task of minified from source and generate distribution file.
 * 
 */
task("minified", function () {
  return src(["src/firebase.js", "src/index.js"])
    .pipe(concat("index.min.js"))
    .pipe(eslint())
    .pipe(terser())
    .pipe(dest("public/"));
});

/**
 * 
 * Task of build from source and generate distribution file.
 * 
 */
task("build", function () {
  return src("src/index.html")
    .pipe(htmlmin({ collapseWhitespace: true }))
    .pipe(dest("public/"));
});

/**
 * 
 * Default task.
 * 
 */
task("default", series(parallel("lib/exceljs", "lib/firebase", "lib/material-components-web", "lib/material-design-icons", "lib/material-icons", "lib/moment", "lib/moment-duration-format", "lib/polyfill"), "minified", "build"));