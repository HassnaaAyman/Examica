import React from 'react';
import './AboutUs.css';
import AboutUsTeam from './AboutUsTeam';
const AboutUs = () => {
  return (
    <div className="AboutUs">
      <div className="AboutUs-Ourvision">
        <div className="AboutUs-headLine">Our Vision</div>
        <div className="AboutUs-Content">
          <p className="content">
            We hope to make exams' building and managing better and smarter
          in order to make the life of the examiners as well as the examinees
  </p>
        </div>

      </div>
      <div className="AboutUs-Section">

        <div className="AboutUs-headLine">AboutUs</div>

        <div className="AboutUs-Content container">
          <p className="content">

            this is a website dedicated to build many different types of exams
            with the easiest way possible with all types of questions and it
            gives you full control over the management of the website from
            starting your account, allowing you to  assign roles to other
            people so as to manage the account with you, building your
            exams ending with getting the results
  </p>
        </div>
      </div>

      <div className="AboutUs-headLine">Our Team</div>

       <div className="AboutUs-img container">
        <AboutUsTeam name={"Omar"} imageUrl={`https://scontent-hbe1-1.xx.fbcdn.net/v/l/t1.0-9/44790457_10217499749750156_3425456612943331328_n.jpg?_nc_cat=109&_nc_oc=AQls58ttxrDtbkovPhWxXeO1A1PfXI-wYh2IEM-Fr13r67Tt-70kY_G2YhQOts98qFI&_nc_ht=scontent-hbe1-1.xx&oh=7917438058034d335799659a63cbb12f&oe=5D83D375`} title={"Developer"} />
        <AboutUsTeam name={"Aya"} imageUrl={`https://scontent-hbe1-1.xx.fbcdn.net/v/t1.0-9/56691321_2411283089106523_7271458835619905536_n.jpg?_nc_cat=101&_nc_oc=AQkNDSvnvYWrpGYvyOPtjNqDpv7jAAfJCQRFz5IpPnHcBxFjRCvki58CbiKclBep4NY&_nc_ht=scontent-hbe1-1.xx&oh=c919e1e2a011117950abad8e9ee4519b&oe=5D849C43`} title={"Developer"} />
        <AboutUsTeam name={"Dina"} imageUrl={`https://media.licdn.com/dms/image/C4D03AQGbOwYW3R4ojg/profile-displayphoto-shrink_800_800/0?e=1567036800&v=beta&t=qTPlvISy-A1Hsw1y96ny0hApNP4_IMjGApGSg7j9RgE`} title={"Developer"} />
        <AboutUsTeam name={"Fatma"} imageUrl={`https://scontent-hbe1-1.xx.fbcdn.net/v/t1.0-9/20246102_1989836537954016_7196722238134746772_n.jpg?_nc_cat=108&_nc_oc=AQnA7GhBMUmVIx65M-qGijBvLVZqqpcbHHUhOcLhxdX5VfTZM_dXNNdZCuu6pZl8k_I&_nc_ht=scontent-hbe1-1.xx&oh=9e76b9b2727b7ade91bf5d6f57c56fad&oe=5D8B9372`} title={"Developer"} />
        <AboutUsTeam name={"Hasnaa"} imageUrl={`https://scontent-hbe1-1.xx.fbcdn.net/v/t1.0-9/39745208_1559624944181542_17106545502322688_n.jpg?_nc_cat=105&_nc_oc=AQmdl0XRVsTKfc6YEhDJx_3y8LMOsIFYtRsWceosZgxD4yyrsCOFAoZw3SY4rDjQDmw&_nc_ht=scontent-hbe1-1.xx&oh=9d960ac056a473f4e5ca1beec2a69388&oe=5D7AE854`} title={"Developer"} />
        <AboutUsTeam name={"Sara"} imageUrl={`https://i.ibb.co/d5pbqW3/me.jpg`} title={"Developer"} />
       </div>




    </div>




  )
};

export default AboutUs;
