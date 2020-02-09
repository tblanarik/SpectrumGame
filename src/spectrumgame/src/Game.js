import React, { Component } from 'react'
import { Link } from 'react-router-dom'

export default function HomePage() {
    return (
        <div className="container">
            <h1>Spectrum</h1>
            <p>
                <Link to="/give">Give a clue!</Link>
            </p>
            <p>
                <Link to="/guess">Make a guess!</Link>
            </p>
        </div>
    )

}