import { CommonModule, registerLocaleData } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';



import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthGuard, AuthInterceptor } from './shared';
import { LanguageTranslationModule } from './shared/modules/language-translation/language-translation.module';

// Thay doi ngon ngu cua ant design
// https://ng.ant.design/docs/i18n/en
import vi from '@angular/common/locales/vi';
registerLocaleData(vi);
import { en_US, NZ_I18N, vi_VN } from 'ng-zorro-antd/i18n';


import { NzIconModule } from 'ng-zorro-antd/icon';

@NgModule({
    imports: [
        CommonModule,
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        LanguageTranslationModule,
        AppRoutingModule
    ],
    declarations: [	AppComponent
   ],
    providers: [
        AuthGuard,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
        },
        { provide: NZ_I18N, useValue: vi_VN }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
