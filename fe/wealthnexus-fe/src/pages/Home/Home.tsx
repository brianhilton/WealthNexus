import { useState } from "react";
import {useForm} from "react-hook-form"
import * as backend from "../../services/wealthnexus.service"

type FormData = {
    userName: string;
    password: string;
  };

type UserDto = {
    id: string,
    username: string,
    password: string,
    email: string,
    phoneNumber: string,
    address: string,
    city: string,
    firstName: string,
    lastName: string,
    accountBalance: number,
    interestRate: number,
    creationDate: string
}

const Home = () => {
    const { register, setValue, handleSubmit, formState: { errors } } = useForm<FormData>();

    const [user, setUser] = useState<UserDto>()

    const onSubmit = handleSubmit(async data => {
        console.log(`try login with ${data.userName} ${data.password}`)
        const response = await backend.login(data.userName, data.password);
        if(response === undefined) return;
        setUser(response)
    });


if(user === undefined){
    return (
        <form onSubmit={onSubmit}>
      <label>Username</label>
      <input {...register("userName")} />
      <div/>
      <label>Password</label>
      <input {...register("password")} />
      <input type="submit"/>
    </form>
    )
}
return (
    <div>
        <h1>Username: {user.username}</h1>
        <h3>Balance: {user.accountBalance}</h3>
    </div>
)
}

export default Home;