package meta;

import meta.log.LoggerUtil;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class Starter {

    public static void main(String[] args) throws InterruptedException {
        SpringApplication.run(Starter.class, args);

        LoggerUtil.error("test");

        Thread.sleep(Long.parseLong("10000"));

    }
}
