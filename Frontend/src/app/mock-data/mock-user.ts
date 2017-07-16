/**
 * Created by User on 02/01/2017.
 */
import {User} from '../User'

export const USER_Login_Response1 = {
    flag:"success",
    user: {
        uid: "12",
        firstname: "hf",
        JWT: "inocandiuiabgd",
    },
    reason: "",
};

export const USER_Login_Response2 = {
    flag:"fail",
    user: {
        uid: "",
        firstname: "",
        JWT: "",
    },
    reason: "wrong pwd",
};

export const USER_Reg_Response1 = {
    flag:"fail",
    user: {
        uid: "",
        firstname: "",
        JWT: "",
    },
    reason: "you repeat yourself",
};

export const USER_Reg_Response2 = {
    flag:"success",
    user: {
        uid: "12",
        firstname: "hf",
        JWT: "inocandiuiabgd",
    },
    reason: "",
};

