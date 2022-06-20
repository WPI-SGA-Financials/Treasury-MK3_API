package edu.wpi.sga.treasury.api.config;

import edu.wpi.sga.treasury.application.enums.Role;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Profile;
import org.springframework.http.HttpMethod;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.builders.WebSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;

@Profile("!auth-disabled")
@Configuration
@EnableWebSecurity
public class AuthSecurityConfig extends WebSecurityConfigurerAdapter {

    @Override
    public void configure(HttpSecurity httpSecurity) throws Exception {
        String BASE_V1_URL = "api/v1/";

        httpSecurity
                .authorizeRequests()
                .mvcMatchers(HttpMethod.GET, BASE_V1_URL + "metadata/*").permitAll()
                .mvcMatchers(HttpMethod.GET, BASE_V1_URL + "financials/budgets").hasRole(Role.SENATE.name());
    }
}

