namespace Models {
    class User {
        private string id;
        private string name;
        
        private string phone;

        private Sex sex;

        public string GetSex() {
            if (sex == Sex.THEYTHEM) {
                return "They/them";
            } else {
                throw new Exception("No valid sex");
            }
        }
    }


    enum Sex {
        THEYTHEM
    }
}