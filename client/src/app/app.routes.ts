import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './member/member-list/member-list.component';
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberEditComponent } from './member/member-edit/member-edit.component';
import { GoogleAuthComponent } from './google-auth/google-auth.component';
import { DataWithGoogleAuthComponent } from './data-with-google-auth/data-with-google-auth.component';
import { RegisterComponent } from './register/register.component';


export const routes: Routes = [
    {path:'',component:HomeComponent},
    {path:'google',component:GoogleAuthComponent},
    {path:'google/data',component:DataWithGoogleAuthComponent},
    {path:'register',component:RegisterComponent},
    {
        path:'',
        runGuardsAndResolvers:'always',
        children:[
            {path:'members',component:MemberListComponent},
            {path:'members/:username',component:MemberDetailComponent},
            {path:'member/edit',component:MemberEditComponent},
            {path:'lists',component:ListsComponent},
            {path:'messages',component:MessagesComponent}
        ]
    },
    
    
    {path:'**',component:HomeComponent,pathMatch:'full'},
];
