export class Employee {
    id : string;
    login : string;
    name : string;
    salary : number;

    isValid() : boolean
    {
        if(this.id && this.login && this.name && this.salary)
        {
            return true;
        }
        return false;
    }
}
