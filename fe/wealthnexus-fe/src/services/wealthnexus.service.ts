import axios from "axios"

export const login = async (username: string, password: string) => {
    try {
        const response = await axios.post("http://192.168.1.5:8080/api/Accounts/login", {username, password});
        console.log(response.headers)
        if(response.status !== 200){
            console.log("error occurred logging in")
            return undefined;
        }
        return response.data;
    } catch (error) {
        console.log(error)
        return undefined;
    }
}