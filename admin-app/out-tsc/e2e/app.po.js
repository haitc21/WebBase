"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppPage = void 0;
var protractor_1 = require("protractor");
var AppPage = /** @class */ (function () {
    function AppPage() {
    }
    AppPage.prototype.navigateTo = function () {
        return protractor_1.browser.get(protractor_1.browser.baseUrl);
    };
    AppPage.prototype.getTitleText = function () {
        return protractor_1.element(protractor_1.by.css('app-root sb-dashboard-head h1')).getText();
    };
    return AppPage;
}());
exports.AppPage = AppPage;
//# sourceMappingURL=app.po.js.map