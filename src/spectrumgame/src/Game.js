import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import ButtonAppBar from './Header'

export default function HomePage() {
    return (
        <div className="container">
            <ButtonAppBar />
            <p>
                <Link to="/give">Give a clue!</Link>
            </p>
            <p>
                <Link to="/guess">Make a guess!</Link>
            </p>
        </div>
    )

}